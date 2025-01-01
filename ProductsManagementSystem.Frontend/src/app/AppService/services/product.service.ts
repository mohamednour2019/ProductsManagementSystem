import { Injectable } from "@angular/core";
import { Subscription, tap } from "rxjs";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Product } from "../models/product.model";
import { SearchDto } from "../models/api-models/search-dto.model";
import { PageList } from "../models/api-models/page-list.model";
import { ApiResponse } from "../models/api-models/api-response.model";

@Injectable({
    providedIn: "root"
})
export class ProductService {

    subscritpion: Subscription;
    url: string = environment.apiBaseUrl;
    constructor(private httpClient: HttpClient) {
    }


    public listProducts(searchDto: SearchDto<Product>) {
        const requestUrl = this.url + environment.endpoints.list;
        return this.httpClient.post<PageList<Product>>(requestUrl, searchDto)
    }


    public addProduct(Product: Product) {
        const requestUrl = this.url + environment.endpoints.add;
        return this.httpClient.post<ApiResponse<boolean>>(requestUrl, Product)
    }

    public editProduct(Product: Product) {
        return this.httpClient.put<ApiResponse<boolean>>(this.url, Product)
    }

    public deleteProduct(id: number) {
        return this.httpClient.delete<ApiResponse<boolean>>(this.url, { params: { id: id } })
    }

    public getProduct(id: number) {
        return this.httpClient.get<ApiResponse<Product>>(this.url, { params: { id: id } });
    }
}