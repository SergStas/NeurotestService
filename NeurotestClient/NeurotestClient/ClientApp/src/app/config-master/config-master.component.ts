import { Component, OnInit } from '@angular/core';
import {TestConfigService} from "../shared/test-config.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-config-master',
  templateUrl: './config-master.component.html',
  styleUrls: ['./config-master.component.css']
})
export class ConfigMasterComponent implements OnInit {
  totalCount = 90;
  duration = 5;

  happinessCount = 5;
  sadnessCount = 5;
  excitedCount = 5;
  fearCount = 5;
  disgustingCount = 5;
  angerCount = 5;

  errorMessage = '';

  constructor(public testConfigService: TestConfigService, private router: Router) { }

  ngOnInit(): void {
  }

  submit() {
    if (!this.validate())
      return;

    this.testConfigService.setTestParams({
      QuestionDuration: this.duration.toString(),
      HappinessCount: this.happinessCount.toString(),
      SadnessCount: this.sadnessCount.toString(),
      FearCount: this.fearCount.toString(),
      AstonishmentCount: this.excitedCount.toString(),
      AngerCount: this.angerCount.toString(),
      DisgustCount: this.disgustingCount.toString(),
    })

    this.router.navigate(['test-module'])
  }

  back() {
    this.router.navigate(['/client-form']);
  }

  private validate() {
    if (this.duration < 3 || this.duration > 7)
      this.errorMessage = 'Время на вопрос должно составлять от 3 до 7 секунд'
    else {
      this.errorMessage = '';
      return true;
    }
    return false;
  }
}
