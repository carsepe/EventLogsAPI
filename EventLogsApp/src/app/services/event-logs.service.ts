// event-logs.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventLogsService {

  private apiUrl = 'https://localhost:7136/api/EventLogs';

  constructor(private http: HttpClient) {}

  registerEvent(event: any): Observable<any> {
    const headers = new HttpHeaders().set('X-Source', 'Formulario');
    return this.http.post(this.apiUrl, event, { headers });
  }

  getEventLogs(eventType: string, startDate: string, endDate: string): Observable<any[]> {
    let params = new HttpParams();

    if (eventType && eventType !== 'Todos') {
        params = params.set('eventType', eventType);
    }

    if (startDate) {
        params = params.set('startDate', startDate);
    }
    if (endDate) {
        params = params.set('endDate', endDate);
    }

    return this.http.get<any[]>(this.apiUrl, { params: params });
}

}
