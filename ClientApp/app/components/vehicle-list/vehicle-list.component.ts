import { Component, OnInit } from '@angular/core';
import { VehicleService } from "../../services/vehicle.service";
import { IVehicle } from "../../models/vehicle";

@Component({
    selector: 'app-vehicle-list',
    templateUrl: './vehicle-list.component.html',
    styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
    makes: any[];
    queryResult: any ={};
    filter: any = {
        pageSize:3
    };
    columns = [
        { title: 'Id' },
        { title: 'Contact Name', key: 'contactName', isSortable: true },
        { title: 'Make', key: 'make', isSortable: true },
        { title: 'Model', key: 'model', isSortable: true },
        {}
    ];
    constructor(private vehicleService: VehicleService) {

    }
    populateVehicle() {
        this.vehicleService.getVehicles(this.filter)
            .subscribe(result => {
                this.queryResult = result;
                console.log(this.queryResult.items);
            });

    }
    ngOnInit() {
        this.vehicleService.getMakes().subscribe(makes => this.makes = makes);
        this.populateVehicle();

    }

 

    onFilterChange() {
        // filter in the client
        //var vehicles = this.allVehicles;
        //console.log(this.filter.makeId);
        //if (this.filter.makeId) {
        //    vehicles = vehicles.filter(v => v.make.id == this.filter.makeId);
        //}
        //console.log(vehicles);
        //if (this.filter.modelId) {
        //    vehicles = vehicles.filter(v => v.model.id == this.filter.modelId);
        //}
        //this.vehicles = vehicles;
        this.filter.page = 1;
        this.filter.pageSize = 3;
        this.populateVehicle();

    }

    resetFilter() {
        this.filter = {};
        this.onFilterChange();
    }

    sortBy(columnName: any) {
        if (this.filter.sortBy === columnName)
            this.filter.isAscending = !this.filter.isAscending;
        else {
            this.filter.sortBy = columnName;
            this.filter.isAscending = true;
        }
        this.populateVehicle();

    }

}
