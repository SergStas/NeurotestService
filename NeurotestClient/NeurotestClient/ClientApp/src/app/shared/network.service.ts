import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { SubjectInfo } from "./client.service";
import { TestConfig } from "./test-config.service";

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

export class WatchInfo {
  video: VideoInfo;
  startTime: string;
  endTime: string;
}

export class VideoSessionResult {
  watchSession: WatchInfo[];
  subjectId: string;
}

@Injectable({ providedIn: 'root' })
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
    return this.httpClient.get('api/video');
    /*return [
      { type: 'Happiness', name: 'hap1.mp4', url: 'http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4' },
      { type: 'Happiness', name: 'hap2.mp4', url: 'http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4' },
      { type: 'Sadness', name: 'sad.mp4', url: 'http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerFun.mp4' },
      { type: 'Astonishment', name: 'ast1.mp4', url: 'http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerJoyrides.mp4' },
      { type: 'Fear', name: 'fear1.mp4', url: 'http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerMeltdowns.mp4' },
      { type: 'Disgust', name: 'dis1.mp4', url: 'http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/Sintel.mp4' }
    ];*/
  }

  getDefaultVideos(id: number) {
    return this.httpClient.get('api/video' + '/' + id);
    /*return [
      { type: 'Happiness', name: 'hap2.mp4', url: 'http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerBlazes.mp4' },
      { type: 'Sadness', name: 'sad.mp4', url: 'http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4' }
    ];*/
  }

  submitResult(result: VideoSessionResult) { //TODO
    return this.httpClient.post('api/video', result);
    /*console.log(result);
    console.log('Session result submitted');*/
  }
}
