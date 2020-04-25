import { Component, OnInit, EventEmitter, Output, OnDestroy } from '@angular/core';
import { AuthService } from 'src/app/auth/auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-sidenav-list',
  templateUrl: './sidenav-list.component.html',
  styleUrls: ['./sidenav-list.component.scss']
})
export class SidenavListComponent implements OnInit, OnDestroy {
  @Output()
  public closeSidenav = new EventEmitter<void>();

  public isAuth = false;

  private authSubscription: Subscription;

  public constructor(private readonly authService: AuthService) {}

  public ngOnInit(): void {
    this.authSubscription = this.authService.authChange.subscribe(authStatus => {
      this.isAuth = authStatus;
    });
  }

  public ngOnDestroy(): void {
    this.authSubscription.unsubscribe();
  }

  public onLogout(): void {
    this.onClose();
    this.authService.logout();
  }

  public onClose(): void {
    this.closeSidenav.emit();
  }
}
