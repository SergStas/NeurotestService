import {Injectable} from "@angular/core";
import {saveAs} from'file-saver';
import {formatDate} from "@angular/common";

@Injectable({providedIn: 'root'})
export class FileService {
  get testCSVString(){
    return this._test;
  }
  set setTestCSVString(value: String) {
    this._test = this._testHeaders + '\n' + value
  }
  private _test: string
  set setVideoCSVString(value: string) {
    this._video = this._videoHeaders + '\n' + value
  }
  get videoCSVString(){
    return this._video;
  }
  private _video: string

  saveTestResult(): boolean {
    return this.saveFile(this._test)
  }

  saveVideoResult(): boolean {
    return this.saveFile(this._video)
  }

  private _videoHeaders = 'Name;Type;Start;End';
  private _testHeaders = 'SubjectID;AngerAcc/Piece;AstonishmentAcc/Piece;DisgustAcc/Piece;FearAcc/Piece;HappinessAcc/Piece;SadnessAcc/Piece;AngerAcc/Percent;AstonishmentAcc/Percent;DisgustAcc/Percent;FearAcc/Percent;HappinessAcc/Percent;SadnessAcc/Percent;UnansweredCount;UnansweredPercentage;SimilarCount;AngerDuration;AstonishmentDuration;DisgustDuration;FearDuration;HappinessDuration;SadnessDuration;MinAngerSpeed;MinAstonishmentSpeed;MinDisgustSpeed;MinFearSpeed;MinHappinessSpeed;MinSadnessSpeed;MeanAngerSpeed;MeanAstonishmentSpeed;MeanDisgustSpeed;MeanFearSpeed;MeanHappinessSpeed;MeanSadnessSpeed;MaxAngerSpeed;MaxAstonishmentSpeed;MaxDisgustSpeed;MaxFearSpeed;MaxHappinessSpeed;MaxSadnessSpeed;WeakAngerMeanSpeed;AverageAngerMeanSpeed;StrongAngerMeanSpeed;WeakAstonishmentMeanSpeed;AverageAstonishmentMeanSpeed;StrongAstonishmentMeanSpeed;WeakDisgustMeanSpeed;AverageDisgustMeanSpeed;StrongDisgustMeanSpeed;WeakFearMeanSpeed;AverageFearMeanSpeed;StrongFearMeanSpeed;WeakHappinessMeanSpeed;AverageHappinessMeanSpeed;StrongHappinessMeanSpeed;WeakSadnessMeanSpeed;AverageSadnessMeanSpeed;StrongSadnessMeanSpeed';

  parseCSV(csv: String): String[][] {
    const rowsCount = csv.split('\n')[0].split(';').length;
    const result = [];
    for (let i=0; i<rowsCount; i++)
      result.push(csv.split('\n').map(e => e.split(';')[i]));
    return result;
  }

  private saveFile(content: string) {
    if (!content)
      return false;
    const blob = new Blob([content], { type: 'text/plain' });
    const name = `Neurotest_${content == this._test ? 'test' : 'video'}_${
      formatDate(Date.now(), 'dd_mm_yyyy', 'en-US').toString()
    }.csv`;
    saveAs(blob, name);
    return true
  }
}
