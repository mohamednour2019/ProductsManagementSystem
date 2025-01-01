import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { TooltipModule } from 'primeng/tooltip';
import { Product } from '../../AppService/models/product.model';
import { DatePipe } from '@angular/common';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ProductService } from '../../AppService/services/product.service';
import { SearchDto } from '../../AppService/models/api-models/search-dto.model';
import { PageList } from '../../AppService/models/api-models/page-list.model';
import { PaginatorModule } from 'primeng/paginator';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-products-list',
  imports: [TableModule, ButtonModule, TooltipModule, DatePipe, CommonModule, PaginatorModule, FormsModule],
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})




export class ProductsListComponent implements OnInit {
  pageNumber: number = 1;
  pageSize: number = 5;
  searchName: string = '';
  productsPageList: PageList<Product> = {
    dataList: [],
    totalCount: 0
  };

  searchDto: SearchDto<Product>;

  sortField: string | null = null;
  sortOrder: number = 0; // 0 for ascending, 1 for descending
  expandedProduct: any = null;

  constructor(private productService: ProductService, private router: Router, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.getProductList();
  }

  getProductList() {
    // Construct the searchDto with filter, sorting, and pagination
    this.searchDto = {
      paginator: {
        PageNumber: this.pageNumber,
        PageSize: this.pageSize
      },
      sorting: {
        sortingColumn: this.sortField,
        sortingDirection: this.sortOrder
      },
      filter: {
        creationDate: new Date(),
        id: 0,
        description: null,
        name: this.searchName ?? null,
        price: 0
      }
    };

    // Call the productService to get the list of products based on searchDto
    this.productService.listProducts(this.searchDto).subscribe({
      next: response => {
        this.productsPageList.dataList = response.dataList;
        this.productsPageList.totalCount = response.totalCount
        this.cdr.detectChanges();
      }
    });
  }


  filterByName(event: any) {
    this.searchName = event.target.value; // Update the searchName property
    this.pageNumber = 1; // Reset to the first page for new search
    this.getProductList(); // Fetch filtered data
  }


  // Add Product Logic
  addProduct() {
    this.router.navigate(['/add-edit']);  // Navigate to add form
  }

  // Edit Product Logic
  editProduct(product: Product) {
    this.router.navigate(['/add-edit'], { queryParams: { id: product.id } });  // Navigate to edit form with product ID
  }

  // Delete Product Logic
  deleteProduct(id: number) {
    this.productService.deleteProduct(id).subscribe({
      next: () => {
        this.getProductList();

      }
    });
  }

  // Toggle Row Logic
  toggleRow(product: any) {
    this.expandedProduct = this.expandedProduct === product ? null : product;
  }

  // Sorting Logic
  changeSort(field: string) {
    if (this.sortField === field) {
      this.sortOrder = this.sortOrder === 0 ? 1 : 0; // Toggle between 0 (ascending) and 1 (descending)
    } else {
      this.sortField = field;
      this.sortOrder = 0; // Default to ascending
    }

    // Update the searchDto with new sorting details
    this.searchDto.sorting = {
      sortingColumn: this.sortField,
      sortingDirection: this.sortOrder
    };

    // Fetch the product list with updated sorting
    this.getProductList(); // Call the method to refetch data with updated sorting
  }

  onPageChange(event: any) {
    this.pageNumber = event.page + 1; // PrimeNG paginator uses 0-based page numbers
    this.pageSize = event.rows;

    // Update the paginator in searchDto
    if (this.searchDto) {
      this.searchDto.paginator = { PageNumber: this.pageNumber, PageSize: this.pageSize };
    }

    // Fetch the product list with updated pagination
    this.getProductList();
  }


}


