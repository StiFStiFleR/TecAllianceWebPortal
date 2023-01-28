
export class Note {
    id : number;
    description : string;
    isDone : boolean;
    isOnEditMode: boolean;

    constructor(init?:Partial<Note>) {
        Object.assign(this, init);
    }
}