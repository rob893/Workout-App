import { Indexable, Primitive } from '../common';

export interface OffsetPagination extends Indexable<Primitive | string[] | number[] | boolean[] | null | undefined> {
  pageNumber?: number;
  pageSize?: number;
}

export interface CursorPagination extends Indexable<Primitive | string[] | number[] | boolean[] | null | undefined> {
  first?: number;
  after?: string;
  last?: number;
  before?: string;
  includeTotal?: boolean;
}

export interface WorkoutInvitationQueryParams extends CursorPagination {
  status?: string | null;
}
