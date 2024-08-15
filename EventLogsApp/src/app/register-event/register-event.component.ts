import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EventLogsService } from '../services/event-logs.service';

@Component({
  selector: 'app-register-event',
  templateUrl: './register-event.component.html',
  styleUrls: ['./register-event.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class RegisterEventComponent {
  event = {
    eventDate: '',
    description: '',
    eventType: 'Formulario'
  };

  successMessage: string = '';
  errorMessage: string = ''; // Variable para almacenar el mensaje de error

  constructor(private eventLogsService: EventLogsService) {}

  ngOnInit(): void {
    const now = new Date();
    this.event.eventDate = now.toISOString().slice(0,16); 
  }

  onSubmit() {
    if (!this.event.eventDate || !this.event.description) {
      console.error('Todos los campos son obligatorios');
      return;
    }

    this.eventLogsService.registerEvent(this.event).subscribe(
      response => {
        console.log('Evento registrado:', response);
        this.successMessage = 'El evento se ha registrado exitosamente.';
        this.errorMessage = ''; // Limpiar cualquier mensaje de error anterior
        this.event.description = ''; 

        setTimeout(() => {
          this.successMessage = '';
        }, 3000);
      },
      error => {
        console.error('Error al registrar el evento:', error);
        this.errorMessage = 'No se pudo conectar con el servidor. Por favor, intente de nuevo más tarde.';
        this.successMessage = ''; 

        setTimeout(() => {
          this.errorMessage = '';
        }, 3000); // Limpiar el mensaje de error después de 3 segundos
      }
    );
  }

  clearForm() {
    this.event = {
      eventDate: '',
      description: '',
      eventType: 'Formulario'
    };
    this.successMessage = ''; 
    this.errorMessage = ''; // Limpiar cualquier mensaje de error
  }
}
