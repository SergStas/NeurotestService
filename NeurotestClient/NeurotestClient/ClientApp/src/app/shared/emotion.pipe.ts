import {Pipe, PipeTransform} from "@angular/core";

@Pipe({
  name: 'emotion'
})
export class EmotionPipe implements PipeTransform {
  transform(value: string): any {
    switch (value.toLowerCase()) {
      case 'happiness': return 'счастье';
      case 'sadness': return 'печаль';
      case 'fear': return 'страх';
      case 'astonishment': return 'удивление';
      case 'disgust': return 'отвращение';
      case 'anger': return 'гнев';
      default: return null
    }
  }
}
