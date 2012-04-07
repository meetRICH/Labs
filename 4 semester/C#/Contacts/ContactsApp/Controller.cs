﻿using System.Windows;
using ContactsLib;
using ContactsLib.Entities;
using Microsoft.Win32;

namespace ContactsApp
{
    public class Controller : DependencyObject
    {
        public static DependencyProperty ContactListProperty = DependencyProperty.Register("ContactList",
                                                                                           typeof (ContactList),
                                                                                           typeof (Controller));

        public static DependencyProperty CurrentContactProperty = DependencyProperty.Register("CurrentContact",
                                                                                              typeof (Contact),
                                                                                              typeof (Controller));

        public Controller(string conn)
        {
           ContactList = new ContactList(conn);
            ContactList.Reload();
        }

        public ContactList ContactList
        {
            get { return (ContactList) GetValue(ContactListProperty); }
            set { SetValue(ContactListProperty, value); }
        }

        public Contact CurrentContact
        {
            get { return (Contact) GetValue(CurrentContactProperty); }
            set { SetValue(CurrentContactProperty, value); }
        }

        public void CreateContact(string name)
        {
            var c = new Contact(name);
            ContactList.DefaultGroup.Contacts.Add(c);
            c.Persist();
        }

        public void AddSimpleDetail(string title, string value)
        {
            ContactDetail d = new ContactDetail(title, value);
            CurrentContact.Details.Add(d);
            d.Persist();
        }

        public void RemoveContact()
        {
            CurrentContact.Destroy();
            ContactList.Remove(CurrentContact);
        }

        public void ChangeContactGroup(Contact contact, string g)
        {
            if (contact.Group != g)
            {
                ContactList.Remove(contact);
                ContactList.Add(contact, g);
                CurrentContact = contact;
                contact.Persist();
            }
        }
    }
}