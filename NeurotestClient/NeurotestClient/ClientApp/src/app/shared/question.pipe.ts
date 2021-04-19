import {Pipe, PipeTransform} from "@angular/core";
import {Question} from "./network.service";

@Pipe({
  name: 'question',
  pure: false
})
export class QuestionPipe implements PipeTransform {
  transform(question: Question) {
    return question.Path;
  }
}
