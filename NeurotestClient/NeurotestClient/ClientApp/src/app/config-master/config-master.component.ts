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

  happinessCount = 15;
  sadnessCount = 15;
  excitedCount = 15;
  fearCount = 15;
  disgustingCount = 15;
  angerCount = 15;

  isEmotionCountValid = this.happinessCount + this.sadnessCount + this.excitedCount +
    this.fearCount + this.disgustingCount + this.angerCount === this.totalCount;

  errorMessage = '';

  constructor(public testConfigService: TestConfigService, private router: Router) { }

  ngOnInit(): void {
  }

  checkEmotionCount() {
    this.isEmotionCountValid = (this.happinessCount + this.sadnessCount + this.excitedCount +
      this.fearCount + this.disgustingCount + this.angerCount) == this.totalCount;
  }

  submit() {
    if (!this.validate())
      return;

    this.testConfigService.setTestParams({
      questionDuration: this.duration,
      totalCount : this.totalCount,
      happinessCount : this.happinessCount,
      sadnessCount : this.sadnessCount,
      fearCount : this.fearCount,
      excitedCount : this.excitedCount,
      angerCount : this.angerCount,
      disgustingCount : this.disgustingCount,
    })

    this.router.navigate(['test-module'])
  }

  back() {
    this.router.navigate(['/client-manager']);
  }

  private validate() {
    this.checkEmotionCount();
    if (this.totalCount < 90)
      this.errorMessage = 'Минимальное количество вопросов - 90';
    else if (!this.isEmotionCountValid /*|| !this.isIntensityCountValid*/)
      this.errorMessage = 'Количество вопросов не совпадает';
    else if (this.duration < 3 || this.duration > 7)
      this.errorMessage = 'Время на вопрос должно составлять от 3 до 7 секунд'
    else {
      this.errorMessage = '';
      return true;
    }
    return false;
  }
}
