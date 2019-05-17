/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { WorkoutPlanService } from './workoutPlan.service';

describe('Service: WorkoutPlan', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WorkoutPlanService]
    });
  });

  it('should ...', inject([WorkoutPlanService], (service: WorkoutPlanService) => {
    expect(service).toBeTruthy();
  }));
});
