import { HttpClient, HttpParamsOptions } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Note } from "../_model/note";


@Injectable({
    providedIn: 'root'
})
export class NoteService {
    constructor(
        private http: HttpClient,
    ) {

    }

    getAllNotesForUser() : Observable<Note[]> {
        return this.http.get<Note[]>(environment.serverUrl + 'note', {withCredentials: true });
    }

    createNote(body : Note) {
        return this.http.post<Note>(environment.serverUrl + 'note', body, {withCredentials: true });
    }

    updateNote(body : Note) : Observable<Note> {
        return this.http.put<Note>(environment.serverUrl + 'note', body, {withCredentials: true });
    }

    deleteNote(id : number) {
        return this.http.delete(environment.serverUrl + 'note/' + id, {withCredentials: true });
    }
}