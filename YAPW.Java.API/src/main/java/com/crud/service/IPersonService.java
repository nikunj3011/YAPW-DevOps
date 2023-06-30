package com.crud.service;

import java.util.List;


import com.crud.model.Person;

public interface IPersonService {
	
	public abstract void getAll(); 
	
	public abstract List<Person> listPerson();

	public abstract List<Person> search(String name);

	public abstract String add(Person p);
	
	public abstract String delete (int id);
	
	public abstract String edit (Person p);
}
