export interface Pagination {
    currentPage: number;
    itemsPerPage: number;
    totalItems: number;
    totalPage: number;
}

export class PaginatedResults<T> {
    results: T;
    pagination: Pagination;
}
