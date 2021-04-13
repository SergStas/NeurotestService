import {Injectable} from "@angular/core";

export class Question {
  url: string;
  emotion: string;
  severity: string;
}

export class Answer {
  question: Question;
  userInput?: string;
  elapsedTime: number;
}

@Injectable({providedIn: 'root'})
export class NetworkService {
  getQuestions(): Question[] {
    //TODO
    return [
      {url: 'https://ds04.infourok.ru/uploads/ex/1232/00028331-86fea6e6/img3.jpg', emotion: 'happiness', severity: 'weak'},
      {url: 'https://im0-tub-ru.yandex.net/i?id=13e7993ae691e5cf6a0b65123ef2619e-sr&n=13', emotion: 'sadness', severity: 'weak'},
      {url: 'https://mycooktes.ru/uploads/images/topic/2017/03/22/4084420e37_1000x.jpg', emotion: 'anger', severity: 'weak'},
      {url: 'https://ds05.infourok.ru/uploads/ex/0fed/000fb3d4-23533ca1/hello_html_m6383e9e3.gif', emotion: 'astonishment', severity: 'weak'},
      {url: 'http://yarkopro.ru/d/01_0257_fizika_desyatichnyye_pristavki_130kh80_sm.jpg', emotion: 'fear', severity: 'average'},
      {url: 'https://ds03.infourok.ru/uploads/ex/0c09/00058a48-46210df9/hello_html_m39acc75.png', emotion: 'disgust', severity: 'strong'}
    ];
  }
}
