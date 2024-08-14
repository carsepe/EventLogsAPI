import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { RegisterEventComponent } from './register-event/register-event.component';
import { EventListComponent } from './event-list/event-list.component';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HttpClientModule, RegisterEventComponent, EventListComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'EventLogsApp';
}
