import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import { Answer } from '../shared/network.service';
import {TestingService} from "../shared/testing.service";

@Component({
  selector: 'app-testing-module',
  templateUrl: './testing-module.component.html',
  styleUrls: ['./testing-module.component.css']
})
export class TestingModuleComponent implements OnInit{
  constructor(
    public testingService: TestingService,
    public router: Router
  ) { }

  beginSession() {
    this.testingService.startTest();
  }

  abort() {
    this.testingService.abortTest();
  }

  back() {
    this.abort();
    this.router.navigate(['config-master']);
  }

  ngOnInit(): void {
    this.testingService.setup();
  }

  rerun() {
    this.testingService.setup();
  }

  getElapsed(answer: Answer) {
    return Number.parseFloat(answer.ElapsedTime);
  }
}
