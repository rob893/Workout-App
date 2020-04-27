import { Indexable } from '../common';

export interface Paginatable extends Indexable<string | number | boolean | undefined | null> {
  pageNumber?: number;
  pageSize?: number;
}

export interface WorkoutInvitationQueryParams extends Paginatable {
  status?: string | null;
}
