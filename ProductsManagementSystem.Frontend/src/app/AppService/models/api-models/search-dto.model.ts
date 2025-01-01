import { PaginatorDto } from "./paginator.model";
import { SortDto } from "./sort-dto.model";

export interface SearchDto<T> {
    filter?: T;
    sorting?: SortDto;
    paginator: PaginatorDto;
}