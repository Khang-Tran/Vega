import { Component, OnInit } from '@angular/core';
import { VehicleService } from "../../services/vehicle.service";
import { ToastyService } from "ng2-toasty";
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from "rxjs/Observable";
import 'rxjs/add/Observable/forkJoin';
@Component({
    selector: 'app-vehicle-form',
    templateUrl: "./vehicle-form.component.html",
    styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
    makes: any[];
    models: any[];
    vehicle: any = {
        features: [],
        contact: {}
    };
    features: any[];
    constructor(private vehicleService: VehicleService,
        private toastyService: ToastyService,
        private router: Router,
        private route: ActivatedRoute) {
        route.params.subscribe(p => this.vehicle.id = +p['id']);
    }

    ngOnInit() {

        Observable.forkJoin([
            this.vehicleService.getVehicle(this.vehicle.id),
            this.vehicleService.getMakes(),
            this.vehicleService.getFeatures()
        ]).subscribe(data => {
            this.vehicle = data[0];
            console.log(this.vehicle);
            this.makes = <any[]>(data[1]);
            console.log(this.makes);
            this.features = <any[]>(data[2]);
            console.log(this.features);
        }, err => {
            if (err.status == 404) {
                this.router.navigate(['/home']);
            }
        });
    }

    onMakeChange() {

        var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
        this.models = selectedMake ? selectedMake.models : [];
        delete this.vehicle.modelId;
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

    submit() {
        this.vehicleService.create(this.vehicle)
            .subscribe(
            x => console.log(x));
    }

}
