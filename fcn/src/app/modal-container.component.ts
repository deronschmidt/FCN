import { Component, OnDestroy, ViewEncapsulation } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { EditModalComponent } from './edit-modal/edit-modal.component';

@Component({
  selector: 'app-modal-container',
  template: '',
  encapsulation: ViewEncapsulation.None
})
export class ModalContainerComponent implements OnDestroy {
  destroy = new Subject<any>();
  currentDialog = null;

  constructor(
    private modalService: NgbModal,
    route: ActivatedRoute,
    router: Router
  ) {
    route.params.pipe(takeUntil(this.destroy)).subscribe(params => {
      // When router navigates on this component is takes the params and opens up the photo detail modal
      this.currentDialog = this.modalService.open(EditModalComponent, { size: 'xl' });

      this.currentDialog.componentInstance.photo = params.id;

      // Go back to home page after the modal is closed
      this.currentDialog.result.then(result => {
        router.navigateByUrl('/');
      }, reason => {
        router.navigateByUrl('/');
      });
    });
  }

  ngOnDestroy() {
    this.destroy.next();
  }
}