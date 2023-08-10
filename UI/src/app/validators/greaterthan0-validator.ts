import { AbstractControl } from "@angular/forms";

export class Greaterthan0Validator{
    static ValidateName(control:AbstractControl){
        const value=control.value as number;
        if(value<0){
            return {
                invalidPrice:true
            }
        }
        return null;
        
    }
}