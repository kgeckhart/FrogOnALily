import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { PhotoshootService, IPhotoshoot, Photoshoot, PhotoshootCategory } from '../photoshootService';
import { Observable } from 'rxjs/Observable';

@Component({
    templateUrl: './photoshoots.component.html',
    styleUrls: ['./photoshoots.component.scss'],
    providers: [ PhotoshootService ]
})
export class PhotoshootsComponent implements OnInit {
    photoshoots: Observable<IPhotoshoot[]>;
    dialog: MatDialog;
    constructor(private photoshootService: PhotoshootService, dialog: MatDialog) { }

    ngOnInit() {
        this.photoshoots =  this.photoshootService.getPhotoshoots(undefined);
    }

    openDialog(photoshoot: IPhotoshoot): void {
        console.log('dialog should open');
        this.dialog.open(PhotoshootsComponent, {
            data: photoshoot
        });
    }
}
