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

export class VideoInfo {
  type: string;
  name: string;
  url: string;
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

  getAllVideos() {
    return [
      {type: 'Happiness', name: 'hap1.mp4', url: ''},
      {type: 'Happiness', name: 'hap2.mp4', url: ''},
      {type: 'Sadness', name: 'sad.mp4', url: ''},
      {type: 'Astonishment', name: 'ast1.mp4', url: ''},
      {type: 'Fear', name: 'fear1.mp4', url: ''},
      {type: 'Disgust', name: 'dis1.mp4', url: ''}
    ];
  }

  getDefaultVideos(id: number) {
    return [
      {type: 'Happiness', name: 'hap2.mp4', url: ''},
      {type: 'Sadness', name: 'sad.mp4', url: ''}
    ];
  }
}
