import {Injectable} from "@angular/core";

export class TestConfig {
  questionDuration: number;
  totalCount: number;

  happinessCount: number;
  sadnessCount: number;
  excitedCount: number;
  fearCount: number;
  disgustingCount: number;
  angerCount: number;
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
