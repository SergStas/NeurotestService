import {Injectable} from "@angular/core";

export class TestConfig {
  id?: number;

  duration: number;
  totalCount: number;

  happinessCount: number;
  sadnessCount: number;
  excitedCount: number;
  fearCount: number;
  disgustingCount: number;
  angerCount: number;

  lowCount: number;
  middleCount: number;
  highCount: number;
}

@Injectable({providedIn: 'root'})
export class TestConfigService {
  private testConfig: TestConfig;

  setTestParams(testConfig: TestConfig) {
    this.testConfig = testConfig;
  }
}
