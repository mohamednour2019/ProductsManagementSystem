import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../AppService/services/product.service';
import { Product } from '../../AppService/models/product.model';
import { ApiResponse } from '../../AppService/models/api-models/api-response.model';

@Component({
  selector: 'app-products-add-edit',
  imports: [CommonModule, ButtonModule, InputTextModule, ReactiveFormsModule],
  templateUrl: './products-add-edit.component.html',
  styleUrls: ['./products-add-edit.component.css']
})
export class ProductsAddEditComponent implements OnInit {
  productForm: FormGroup;
  isEditMode: boolean = false; // To determine if we're in edit mode or not
  productId: number | null = null; // Store the product ID

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private productService: ProductService,
    private router: Router
  ) {
    // Initialize form with validation
    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      price: ['', [Validators.required, Validators.min(0.01)]],
    });


    // Check if we have an 'id' in the queryParams
    this.route.queryParams.subscribe(params => {
      const id = params['id'];
      if (id) {
        this.isEditMode = true;
        this.productId = +id; // Convert string to number
        this.loadProductDetails(this.productId); // Load the product details for editing
      }
    });
  }

  ngOnInit(): void {

  }

  goBack() {
    this.router.navigate(['/list']);
  }
  loadProductDetails(id: number): void {
    this.productService.getProduct(id).subscribe({
      next: (response: ApiResponse<Product>) => {
        this.productForm.patchValue({
          name: response.data.name,
          description: response.data.description,
          price: response.data.price,
        });
      }
    });
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      const productData: Product = {
        name: this.productForm.value.name,
        price: this.productForm.value.price,
        description: this.productForm.value.description,
      };
      if (this.isEditMode) {
        productData.id = this.productId
        this.productService.editProduct(productData).subscribe({
          next: () => {
            this.goBack()
          }
        });
      } else {

        this.productService.addProduct(productData).subscribe({
          next: () => {
            this.goBack()
          },

        });
      }
    }
  }
}
