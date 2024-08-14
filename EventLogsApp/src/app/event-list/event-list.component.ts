import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EventLogsService } from '../services/event-logs.service';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class EventListComponent {
  events: any[] = [];
  eventType: string = 'Todos'; // Valor por defecto 'Todos'
  startDate: string = '';
  endDate: string = '';
  errorMessage: string = '';

  constructor(private eventLogsService: EventLogsService) {}

  // Se ejecuta cada vez que se cambia el tipo de evento
  onEventTypeChange() {
    this.clearResults(); // Limpia la tabla cuando cambie el tipo de evento
  }

  onSearch() {
    this.errorMessage = '';

    // Configura los parámetros de búsqueda
    let eventTypeParam = this.eventType !== 'Todos' ? this.eventType : '';

    // Realiza la búsqueda
    this.eventLogsService.getEventLogs(eventTypeParam, this.startDate, this.endDate).subscribe(
        response => {
            this.events = response;
        },
        error => {
            console.error('Error al cargar los eventos:', error);
            this.errorMessage = 'Error al cargar los eventos. Verifique los filtros e intente nuevamente.';
        }
    );
}


  // Método para limpiar los filtros y los resultados
  clearFilters() {
    this.eventType = 'Todos';
    this.startDate = '';
    this.endDate = '';
    this.clearResults();
  }

  // Método para limpiar la tabla de resultados
  clearResults() {
    this.events = [];
    this.errorMessage = '';
  }
}
