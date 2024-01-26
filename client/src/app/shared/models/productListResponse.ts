import { Product } from "./product";

export interface ProductListResponse{
data:Product[],
pageNumber:number,
pageSize:number,
totalCount:number

}