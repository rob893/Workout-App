import { Component, OnInit, EventEmitter, Output, OnDestroy } from '@angular/core';
import { AuthService } from 'src/app/auth/auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy {
  @Output()
  public sidenavToggle = new EventEmitter<void>();

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
    this.authService.logout();
  }

  public onToggleSidenav(): void {
    this.sidenavToggle.emit();
  }
}
