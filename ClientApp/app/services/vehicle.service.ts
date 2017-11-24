import { Injectable } from '@angular/core';
import { Http } from "@angular/http/";
import 'rxjs/add/operator/map'

@Injectable()
export class VehicleService {

    constructor(private http: Http) { }

    getMakes() {
        return this.http.get('/api/makes').map(res => res.json());
    }
    getFeatures() {
        return this.http.get("/api/features")
            .map(res => res.json());
    }
    create(vehicle: any) {
        return this.http.post('/api/vehicles', vehicle).map(res => res.json());
    }

    getVehicle(id: any) {
        return this.http.get('/api/vehicles/' + id).map(res => res.json());
    }

    update(vehicle: any) {
        return this.http.put('/api/vehicles/' + vehicle.id, vehicle).map(res => res.json());
    }

    delete(id: any) {
        return this.http.delete('/api/vehicles/' + id).map(res => res.json());
    }

    getVehicles(filter: any) {

        return this.http.get('/api/vehicles' + '?' + this.toQueryString(filter)).map(res => res.json());
    }

    toQueryString(obj: any) {
        var parts = [];
        for (var prop in obj) {
            if (obj.hasOwnProperty(prop)) {
                var value = obj[prop];
                if (value != null) {
                    parts.push((encodeURIComponent(prop) + '=' + encodeURIComponent(value)) as any);
                }
            }

        }

        return parts.join('&');
    }

}
