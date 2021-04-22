import {Injectable} from "@angular/core";

export class TestConfig {
  QuestionDuration: string;
  AngerCount: string;
  AstonishmentCount: string;
  DisgustCount: string;
  FearCount: string;
  HappinessCount: string;
  SadnessCount: string;
}

@Injectable({providedIn: 'root'})
export class TestConfigService {
  private testParams: TestConfig;

  getTestParams() {
    return this.testParams;
  }

  setTestParams(testConfig: TestConfig) {
    this.testParams = testConfig;
  }
}
