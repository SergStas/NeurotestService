import {Injectable} from "@angular/core";
import {saveAs} from'file-saver';

@Injectable({providedIn: 'root'})
export class FileService {
  saveFile() {
    const blob = new Blob(['text'], { type: 'text/plain' });
    saveAs(blob, 'D:\\');
    console.log("done");
  }
}
