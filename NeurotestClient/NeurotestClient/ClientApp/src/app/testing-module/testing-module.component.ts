import { Component, OnInit } from '@angular/core';
import {TestConfig, TestConfigService} from "../shared/test-config.service";
import {NetworkService, Question} from "../shared/network.service";
import {Router} from "@angular/router";
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
    this.testingService.startTest();
  }
}
