import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { ProductsListComponent } from './modules/products-list/products-list.component';

export const routes: Routes = [
    {
        path: '',
        redirectTo: '/list',
        pathMatch: 'full'
    },
    {
        path: 'list',
        loadComponent: () => import('./modules/products-list/products-list.component').then(m => m.ProductsListComponent)
    },
    {
        path: 'add-edit',
        loadComponent: () => import('./modules/products-add-edit/products-add-edit.component').then(m => m.ProductsAddEditComponent)
    }
];