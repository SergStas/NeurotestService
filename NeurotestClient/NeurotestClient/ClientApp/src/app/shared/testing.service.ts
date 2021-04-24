import {Injectable} from "@angular/core";
import { BehaviorSubject, interval, Subscription } from "rxjs";
import { Answer, NetworkService, Question, QuestionJson } from "./network.service";
import {TestConfigService} from "./test-config.service";
import { ClientService } from "./client.service";

@Injectable({providedIn: 'root'})
export class TestingService {
  private duration: number;
  private sub: Subscription;

  private results = false;
  get displayResults() {
    return this.results;
  }

  private answers: Answer[];
  get testResults() {
    return this.answers;
  }
  get correctCount() {
    return this.answers.filter(a => Number.parseFloat(a.ElapsedTime) >= 0 && a.Question.Type == a.UserInput).length;
  }
  get accuracy() {
    return this.correctCount / this.questionsCount;
  }

  private timer;
  get timeLeft() {
    return this.duration - this.timer;
  }

  private question: BehaviorSubject<Question> = new BehaviorSubject<Question>(null)
  get currentQuestion() {
    return this.question?.value;
  }

  private questions: Question[];
  get questionsCount() {
    return this.questions.length;
  }

  private index;
  get questionNumber() {
    return this.index;
  }

  private test = false;
  get testSession() {
    return this.test;
  }

  private end = false;
  get showEndMessage() {
    return this.end;
  }

  public constructor(
    private configService: TestConfigService,
    private networkService: NetworkService,
    private clientService: ClientService
  ) {}

  setup() {
    this.duration = Number.parseFloat(this.configService.getTestParams().QuestionDuration);
    this.answers = [];
    this.index = 0;
    this.end = false;
    this.results = false;

    this.networkService.getQuestions(this.configService.getTestParams())
      .subscribe(
        (questionsData: QuestionJson[]) => {
          this.questions = questionsData.map(q => this.convertToQuestion(q));
        }
      );
  }

  startTest() {
    this.test = true;
    this.setQuestion();
  }

  abortTest() {
    this.test = false;
    this.setup()
  }

  submitAnswer(answer: string) {
    if (!this.test)
      return;
    this.answers.push({
      Question: this.currentQuestion,
      UserInput: answer,
      ElapsedTime: this.timer.toString()
    });

    this.nextQuestion();
  }

  private nextQuestion() {
    this.index++;
    if (this.questions.length <= this.index)
      this.endTest();
    else
      this.setQuestion();
  }

  private endTest() {
    this.sub?.unsubscribe();

    this.test = false;
    this.end = true;

    this.networkService.saveResult({
      SubjectId: this.clientService.clientId.toString(),
      Answers: this.answers
    }).subscribe();
  }

  private setQuestion() {
    this.sub?.unsubscribe();

    this.question.next(this.questions[this.index]);
    this.timer = 0;
    let timer = interval(10);

    this.sub = timer.subscribe(d => {
      this.timer = d / 100;
      if (Math.abs(this.timer - this.duration) < 1e-5) {
        this.answers.push({
          Question: this.currentQuestion,
          ElapsedTime: '-1',
          UserInput: 'Undefined'
        });
        this.sub.unsubscribe();
        this.nextQuestion();
      }
    })
  }

  showResults() {
    this.results = true;
  }

  convertToQuestion(q: QuestionJson): Question {
    return {
      Url: q.url,
      Type: q.type,
      Severity: q.severity
    }
  }

  isCorrect(a: Answer) {
    return Number.parseFloat(a.ElapsedTime) >= 0 && a.Question.Type.toLowerCase() == a.UserInput.toLowerCase();
  }
}
