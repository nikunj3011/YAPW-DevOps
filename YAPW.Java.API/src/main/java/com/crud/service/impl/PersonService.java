package com.crud.service.impl;

import java.util.List;

import com.crud.model.Person;
import com.crud.repo.PersonRepo;
import com.crud.service.IPersonService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class PersonService implements IPersonService {

	@Autowired
	private PersonRepo personRepo;

	@Override
	public void getAll() {
		// TODO Auto-generated method stub
		personRepo.getAll();

	}

	@Override
	public List<Person> listPerson() {
		return personRepo.listPerson();

	}

	@Override
	public List<Person> search(String name) {
		return personRepo.search(name);

	}

	@Override
	public String add(Person p) {

		return personRepo.add(p);
	}

	@Override
	public String delete(int id) {
		return personRepo.delete(id);
	}

	@Override
	public String edit(Person p) {
		return personRepo.edit(p);
	}

}
