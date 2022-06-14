﻿using HiooshServer.Models;

namespace HiooshServer.Services
{
    public class ContactService : IContactsService
    {
        
        private static List<User> users = new List<User>();

        // constructor
        public ContactService()
        {
            Message m1 = new Message(1, "hi", true, "05/24/2022 08:48 AM", "Text");
            Message m2 = new Message(2, "bye", false, "05/24/2022 08:50 AM", "Text");
            Contact contHadasForHail = new Contact("hadas", "doosa", "localhost:5034");
            contHadasForHail.chat.Add(m1);
            contHadasForHail.chat.Add(m2);
            Contact contShiraForHail = new Contact("shira", "shiroosh", "localhost:5034");
            contShiraForHail.chat.Add(m1);
            contShiraForHail.chat.Add(m2);
            Contact contHailForHadas = new Contact("Hail", "hailos", "localhost:5034");
            contHailForHadas.chat.Add(new Message(1, "hi", false, "17.5.22", "Text"));
            contHailForHadas.chat.Add(new Message(2, "bye", true, "17.5.22", "Text"));
            Contact contHailForShira = new Contact("Hail", "hailos", "localhost:5034");
            contHailForShira.chat.Add(new Message(1, "hi", false, "17.5.22", "Text"));
            contHailForShira.chat.Add(new Message(2, "bye", true, "17.5.22", "Text"));
            User user1 = new User("Hail", "Aa123456", "hailoosh", null);
            user1.Contacts.Add(contHadasForHail);
            user1.Contacts.Add(contShiraForHail);
            User user2 = new User("hadas", "Aa123456", "doosa", null);
            user2.Contacts.Add(contHailForHadas);
            User user3 = new User("shira", "Aa123456", "shroslosh", null);
            user3.Contacts.Add(contHailForShira);
            User user4 = new User("ortal", "Aa123456", "ortaloosh", null);
            users.Add(user1);
            users.Add(user2);
            users.Add(user3);
            users.Add(user4);
        }

        public User? GetUser(string id)
        {
            return users.Find(x => x.Id == id);
        }

        public List<User> GetUsers()
        {
            return users;
        }
        public void AddUser(User user)
        {
            users.Add(user);
        }

        public IEnumerable<Contact> GetAllContacts(string userID)
        {
            User user = GetUser(userID);
            return user.Contacts;
        }


        public void AddContact(string userID, Contact contact)
        {
            User user = GetUser(userID);
            user.Contacts.Add(contact);
        }

        public Contact? GetContact(string userID, string contactID)
        {
            User user = GetUser(userID);
            if (user.Contacts != null)
            {
                return user.Contacts.Find(x => x.id == contactID);
            }
            return null;
        }

        public void RemoveContact(string userID, string contactID)
        {
            User user = GetUser(userID);
            Contact contact = GetContact(userID, contactID);
            if (contact != null)
            {
                user.Contacts.Remove(contact);
            }
            
        }
        public void UpdateContact(string userID, string contactID, string nickname, string server)
        {
            Contact contact = GetContact(userID, contactID);
            if (contact != null)
            {
                contact.name = nickname;
                contact.server = server;
            }
        }

        // get the chat with the contact with this id
        public List<Message> GetMessages(string userID, string contactID)
        {
            Contact contact = GetContact(userID, contactID);
            return contact.chat;
        }

        // add message to the chat with the contact with this id
        public void AddMessage(string userID, string contactID, Message message)
        {
            Contact? contact = GetContact(userID, contactID);
            if (contact != null)
            { 
                contact.chat.Add(message);
                contact.last = message.content;
                contact.lastdate = message.created;
            }
        }

        public Message? GetMessage(string userID, string contactID, int messageID)
        {
            List<Message> messages = GetMessages(userID, contactID);
            if (messages.Count == 0)
            {
                return null;
            }
            return messages.Find(x => x.id == messageID);
        }
        public void RemoveMessage(string userID, string contactID, int messageID)
        {
            Message? message = GetMessage(userID, contactID, messageID);
            if (message == null)
            {
                return;
            }
            List<Message> messages = GetMessages(userID, contactID);
            messages.Remove(message);
        }
        public void UpdateMessage(string userID, string contactID, int messageID, string content)
        {
            Message message = GetMessage(userID, contactID, messageID);
            if (message != null) 
            {
                message.content = content;
            }
        }
    }
}
