import * as Raven from 'raven-js';
import { ToastyService } from 'ng2-toasty';
import { ErrorHandler, Inject, isDevMode } from '@angular/core';

export class AppErrorHandler implements ErrorHandler {
    constructor(
      
        @Inject(ToastyService) private toastyService: ToastyService) {
    }

    handleError(error: any): void {
        if (!isDevMode())
            Raven.captureException(error.originalError || error);
        else
            throw error;


        if (typeof (window) !== 'undefined') {
            this.toastyService.error({
                title: 'Error',
                msg: 'An unexpected error happened.',
                showClose: true,
                timeout: 5000,
                theme: 'bootstrap'
            });

        }

    }
}