export interface CursorPaginatedResponse<T> {
  edges: Edge<T>[];
  nodes: T[];
  pageInfo: PageInfo;
  totalCount: number | null;
}

export interface Edge<T> {
  cursor: string;
  node: T;
}

export interface PageInfo {
  startCursor: string;
  endCursor: string;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}

export interface WorkoutAppAPIError {
  errors: string[];
  type: string;
  title: string;
  status: number;
  detail: string;
  instance: string;
  extensions: any;
}
