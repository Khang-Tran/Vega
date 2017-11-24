import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { VehicleFormComponent } from "./components/vehicle-form/vehicle-form.component";
import { VehicleService } from "./services/vehicle.service";
import { ToastyModule } from 'ng2-toasty';
import { AppErrorHandler } from "./app.error-handler";
import * as Raven from 'raven-js';
import {VehicleListComponent} from "./components/vehicle-list/vehicle-list.component";
import {PaginationComponent} from "./components/pagination/pagination.component";
Raven.config("https://c9a339fe0024411e867f916bb9b17689@sentry.io/249257").install();
@
    NgModule({
        declarations: [
            AppComponent,
            NavMenuComponent,
            CounterComponent,
            FetchDataComponent,
            HomeComponent,
            VehicleFormComponent,
            VehicleListComponent,
            PaginationComponent
        ],
        imports: [
            CommonModule,
            ToastyModule.forRoot(),
            HttpModule,
            FormsModule,
            RouterModule.forRoot([
                { path: '', redirectTo: 'vehicles', pathMatch: 'full' },
                { path: 'vehicles/new', component: VehicleFormComponent },
                { path: 'vehicles/:id', component: VehicleFormComponent },
                { path: 'vehicles', component: VehicleListComponent },
                { path: 'home', component: HomeComponent },
                { path: 'counter', component: CounterComponent },
                { path: 'fetch-data', component: FetchDataComponent },
                { path: '**', redirectTo: 'home' }
            ])
        ],
        providers: [
            VehicleService,
            { provide: ErrorHandler, useClass: AppErrorHandler }
        ]
    })
export class AppModuleShared {
}
