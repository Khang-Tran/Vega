import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { ToastyService } from "ng2-toasty";
import { VehicleService } from "../../services/vehicle.service";
import { PhotoService } from "../../services/photo.service";
import { ProgressService } from "../../services/progress.service";

@Component({
  selector: 'app-view-vehicle',
  templateUrl: './view-vehicle.component.html',
  styleUrls: ['./view-vehicle.component.css']
})
export class ViewVehicleComponent implements OnInit {

    @ViewChild('fileInput') fileInput: ElementRef;
    vehicle:any;
    vehicleId:number;
    photos: any[];
    progress: any;
  constructor(
      private route: ActivatedRoute,
      private router: Router,
      private toasty: ToastyService,
      private vehicleService: VehicleService,
      private photoService: PhotoService,
      private progressService: ProgressService
  ) {
      this.route.params.subscribe(p => {
          this.vehicleId = +p['id'];
          if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
              this.router.navigate(['/vehicles']);
              return;
          }
      });
  }

  ngOnInit() {
      this.vehicleService.getVehicle(this.vehicleId).subscribe(
          v => this.vehicle = v,
          err => {
              if (err.status == 404) {
                  this.router.navigate(['/vehicles']);
                  return;
              }
          }
      );

      this.photoService.getPhotos(this.vehicleId).subscribe(photos => this.photos = photos);
  }

delete() {
    if (confirm("Are you sure want to delete this?")) {
        this.vehicleService.delete(this.vehicle.id)
            .subscribe(x => {
                this.router.navigate(['/vehicle s']);
            });
    }
}

uploadPhoto() {

    this.progressService.startTracking().subscribe(progress => {
        console.log(progress)
        this.progress = progress;
    },(null) as any, ()=>{this.progress = null});


    var nativeElement:HTMLInputElement = this.fileInput.nativeElement;
    let file: any;
    if (nativeElement.files != null) {
        file = nativeElement.files[0];
    }
    nativeElement.value = '';
        this.photoService.upload(this.vehicleId, file).subscribe(
            photo => this.photos.push(photo),
            error => {
                this.toasty.error({
                    title: 'Error',
                    msg: error.text(),
                    showClose: true,
                    timeout: 5000,
                    theme: 'bootstrap'
                });
            }
        );
}

}
