import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Note } from '../../_model/note';
import { NoteService } from '../../_services/note.service';
import { ConfirmationDialogComponent } from '../../_shared-components/confirmation-dialog/confirmation-dialog.component';


@Component({
  selector: 'app-note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.css']
})
export class NoteComponent implements OnInit {
  notes: Note[];
  emailFormControl = new FormControl('', [Validators.required, Validators.email]);

  constructor(
    private noteService: NoteService,
    private router: Router,
    public dialog: MatDialog) {

  }

  ngOnInit(): void {
    this.noteService.getAllNotesForUser().subscribe({
      next: (data) => this.notes = data,
      error: (error) => {
        if (error.status === 401) {
          this.router.navigate(['/']);
        }
        else {
          console.log(error.error);
        }
      }
    });
  }

  createNote(input: HTMLInputElement) {
    const description = input.value;
    if (description && description.length <= 250) {
      const note = new Note({ description: description, isDone: false });
      this.noteService.createNote(note).subscribe((data) => { this.notes.push(data); input.value = '' });
    }
  }

  updateNote(note: Note, description?: string) {
    if (description) {
      note.description = description;
      const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
        width: '250px',
        data: { var: false, message: 'Save changes?' }
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.noteService.updateNote(note).subscribe(() => note.isOnEditMode = false);
        }
      });
    }
    else {
      this.noteService.updateNote(note).subscribe(() => note.isOnEditMode = false);
    }
  }

  deleteNote(note: Note) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '250px',
      data: { var: false, message: 'Delete this ToDo?' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.noteService.deleteNote(note.id).subscribe({
          next: (data) => {
            const index = this.notes.indexOf(note, 0);
            if (index > -1) {
              this.notes.splice(index, 1);
            }
          }
        });
      }
    });
  }

  changeMode(note : Note) {
    note.isOnEditMode = !note.isOnEditMode;
  }
}
