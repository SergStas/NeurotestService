import { HttpClient } from "@angular/common/http";
import {Injectable} from "@angular/core";
import {SubjectInfo} from "./client.service";
import {TestConfig} from "./test-config.service";

export class Question {
  Url: string;
  Type: string;
  Severity: string;
}

export class QuestionJson {
  url: string;
  type: string;
  severity: string;
}

export class Answer {
  Question: Question;
  UserInput: string;
  ElapsedTime: string;
}

export class TestResult {
  SubjectId: string;
  Answers: Answer[];
}

@Injectable({providedIn: 'root'})
export class NetworkService {

  constructor(
    private httpClient: HttpClient
  ) { }

  getClients() {
    return this.httpClient.get('api/subject');
  }

  addClient(client: SubjectInfo) {
    console.log(client);
    return this.httpClient.post('api/subject', client);
  }

  saveResult(result: TestResult) {
    console.log(result);
    return this.httpClient.post('api/result', result);
  }

  getQuestions(params: TestConfig) {
    console.log(params);
    return this.httpClient.post('api/test', params);
  }
}
