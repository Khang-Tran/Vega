import { Component, OnInit } from '@angular/core';
import { VehicleService } from "../../services/vehicle.service";
import { ToastyService } from "ng2-toasty";
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from "rxjs/Observable";
import 'rxjs/add/Observable/forkJoin';
import { ISaveVehicle } from "../../models/saveVehicle";
import { IVehicle } from "../../models/vehicle";
import * as _ from 'underscore';
@Component({
    selector: 'app-vehicle-form',
    templateUrl: "./vehicle-form.component.html",
    styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
    makes: any[];
    models: any[];
    vehicle: ISaveVehicle = {
        id: 0,
        makeId: 0,
        modelId: 0,
        isRegister: false,
        features: [],
        contact: {
            name: '',
            phone: '',
            email: ''
        }
    };
    features: any[];
    constructor(private vehicleService: VehicleService,
        private toastyService: ToastyService,
        private router: Router,
        private route: ActivatedRoute) {
        route.params.subscribe(p => {
            this.vehicle.id = +p['id'] || 0;
        });

    }

    ngOnInit() {
        var sources = [
            this.vehicleService.getMakes(),
            this.vehicleService.getFeatures()
        ];

        if (this.vehicle.id)
            sources.push(this.vehicleService.getVehicle(this.vehicle.id));


        Observable.forkJoin(sources).subscribe(data => {
            this.makes = <any[]>(data[0]);
            this.features = <any[]>(data[1]);
            if (this.vehicle.id) {
                this.setVehicle((data[2]) as any);
                this.populateModels();
            }
        }, err => {
            if (err.status == 404) {
                this.router.navigate(['/home']);
            }
        });
    }


    private setVehicle(vehicle: IVehicle) {
        this.vehicle.id = vehicle.id;
        this.vehicle.makeId = vehicle.make.id;
        this.vehicle.modelId = vehicle.model.id;
        this.vehicle.isRegister = vehicle.isRegister;
        this.vehicle.contact = vehicle.contact;
        this.vehicle.features = _.pluck(vehicle.features, 'id');
    }

    onMakeChange() {


        this.populateModels();
        delete this.vehicle.modelId;
    }

    private populateModels() {
        var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
        this.models = selectedMake ? selectedMake.models : [];
    }

    onFeatureToggle(featureId: any, $event: any) {
        if ($event.target.checked) {
            this.vehicle.features.push(featureId);
        }
        else {
            var index = this.vehicle.features.indexOf(featureId);
            this.vehicle.features.splice(index, 1);
        }
    }
    delete() {
        if (confirm("Are you sure?")) {
            this.vehicleService.delete(this.vehicle.id).subscribe(x => { this.router.navigate(['/home']) });
        }
    }
    submit() {

        if (this.vehicle.id) {
            this.vehicleService.update(this.vehicle)
                .subscribe(x => {
                    this.toastyService.success({
                        title: 'Success',
                        msg: 'The vehicle updated successfully',
                        theme: 'bootstrap',
                        showClose: true,
                        timeout: 5000
                    });
                });
        } else {
            this.vehicleService.create(this.vehicle)
                .subscribe(x =>
                    this.toastyService.success({
                        title: 'Success',
                        msg: 'Created successfully',
                        theme: 'bootstrap',
                        showClose: true,
                        timeout: 5000
                    })
                );

        }


    }

}
