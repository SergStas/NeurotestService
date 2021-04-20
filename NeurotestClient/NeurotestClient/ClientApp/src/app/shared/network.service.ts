import { HttpClient } from "@angular/common/http";
import {Injectable} from "@angular/core";
import {SubjectInfo} from "./client.service";
import {TestConfig} from "./test-config.service";

export class Question {
  Path: string;
  Type: string;
  Severity: string;
}

export class Answer {
  Question: Question;
  UserInput?: string;
  ElapsedTime: number;
}

export class ResultInfo {
  SubjectId: number;
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
    return this.httpClient.post('api/subject', client);
  }

  saveResult(result: ResultInfo) {
    return this.httpClient.post('api/result', result);
  }

  getQuestions(params: TestConfig) {
    return this.httpClient.post('api/test', params);
    /*return [
      {Path: 'https://ds04.infourok.ru/uploads/ex/1232/00028331-86fea6e6/img3.jpg', Type: 'happiness', Severity: 'weak'},
      {Path: 'https://im0-tub-ru.yandex.net/i?id=13e7993ae691e5cf6a0b65123ef2619e-sr&n=13', Type: 'sadness', Severity: 'weak'},
      {Path: 'https://mycooktes.ru/uploads/images/topic/2017/03/22/4084420e37_1000x.jpg', Type: 'anger', Severity: 'weak'},
      {Path: 'https://ds05.infourok.ru/uploads/ex/0fed/000fb3d4-23533ca1/hello_html_m6383e9e3.gif', Type: 'astonishment', Severity: 'weak'},
      {Path: 'http://yarkopro.ru/d/01_0257_fizika_desyatichnyye_pristavki_130kh80_sm.jpg', Type: 'fear', Severity: 'average'},
      {Path: 'https://ds03.infourok.ru/uploads/ex/0c09/00058a48-46210df9/hello_html_m39acc75.png', Type: 'disgust', Severity: 'strong'}
    ];*/
  }
}
