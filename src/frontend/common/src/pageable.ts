export type Pageable<T> = {
  items: T[];
  total: number;
  filteredTotal: number;
  page: number;
  pageSize: number;
};
