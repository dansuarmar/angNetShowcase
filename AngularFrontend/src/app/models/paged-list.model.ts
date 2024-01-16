import { Customer } from './customer.model';

export class PagedList {
  items: Customer[] = []
  page!: number
  size!: number
  total!: number
  hasNext!: boolean
  hasPrev!: boolean
}
