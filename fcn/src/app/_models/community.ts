export class Community {
    id: number;
    communityName: string;
    affiliation: string;
    address1: string;
    address2: string;
    city: string;
    state: string;
    zipCode: string;
    phone: string;
    alternatePhone: string;
    email: string;
    website: string;
    active: boolean;
    contacts: CommunityContact[];
    createdDate: Date;
    updatedDate: Date;
}

export class CommunityContact {
    id: number;
    communityID: number;
    communityName: string;
    contactID: number;
    contactName: string;
    createdDate: Date;
    updatedDate: Date;
}