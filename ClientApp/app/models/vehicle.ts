import {IKeyValuePair} from './keyValuePair';
import {IContact} from './contact';

export interface IVehicle {
    id: number;
    model: IKeyValuePair;
    make: IKeyValuePair;
    isRegister: boolean;
    features: IKeyValuePair[];
    contact: IContact;
    lastUpdate: string;
}