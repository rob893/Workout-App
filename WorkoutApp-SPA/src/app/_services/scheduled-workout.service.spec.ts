/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ScheduledWorkoutService } from './scheduled-workout.service';

describe('Service: ScheduledWorkout', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ScheduledWorkoutService]
    });
  });

  it('should ...', inject([ScheduledWorkoutService], (service: ScheduledWorkoutService) => {
    expect(service).toBeTruthy();
  }));
});
