import {Injectable} from "@angular/core";
import {BehaviorSubject} from "rxjs";
import {Answer, NetworkService, Question} from "./network.service";
import {TestConfigService} from "./test-config.service";

@Injectable({providedIn: 'root'})
export class TestingService {
  question: BehaviorSubject<Question> = new BehaviorSubject<Question>(null)
  get currentQuestion() {
    return this.question?.value;
  }

  private duration: number;
  private questions: Question[];
  private answers: Answer[];
  private index;

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
    private networkService: NetworkService
  ) {}

  setup() {
    this.duration = this.configService.getTestParams().questionDuration;
    this.questions = this.networkService.getQuestions();
    this.answers = [];
    this.index = 0;
    this.end = false;

    this.question.next(this.questions[this.index]);
  }

  startTest() {
    this.test = true;
  }

  abortTest() {
    this.test = false;
  }

  submitAnswer(answer: string) {
    this.answers.push({
      question: this.currentQuestion,
      userInput: answer,
      elapsedTime: 0
    });

    this.nextQuestion();
  }

  private nextQuestion() {
    this.index++;
    if (this.questions.length <= this.index)
      this.endTest();
    else
      this.question.next(this.questions[this.index]);
  }

  private endTest() {
    this.test = false;
    this.end = true;
  }
}
