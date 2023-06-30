package com.crud.repo;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;

import com.crud.model.Person;

import org.springframework.stereotype.Repository;

@Repository
public class PersonRepo {

	private List<Person> list = new ArrayList<Person>();

	public void getAll() {
		List<Person> newList = Arrays.asList(new Person(1, "Trinh Minh Cuong", "Developer", true, "1975-11-27"),
				new Person(2, "Mary Jane", "Banker", false, "1980-05-24"),
				new Person(3, "Tom Sawyer", "Taxi Driver", true, "1990-08-09"));
		for (Person person : newList) {
			list.add(person);
		}

	}

	public List<Person> listPerson() {
		List<Person> newList = Arrays.asList(new Person(1, "Trinh Minh Cuong", "Developer", true, "1975-11-27"),
				new Person(2, "Mary Jane", "Banker", false, "1980-05-24"),
				new Person(3, "Tom Sawyer", "Taxi Driver", true, "1990-08-09"));
		for (Person person : newList) {
			list.add(person);
		}
		return list;
	}

	public List<Person> search(String name) {
		return list.stream().filter(x -> x.getName().startsWith(name)).collect(Collectors.toList());
	}

	public String add(Person p) {
		Person obj = new Person();
		obj.setId(p.getId());
		obj.setName(p.getName());
		obj.setJob(p.getJob());
		obj.setGender(p.isGender());
		obj.setBirthDay(p.getBirthDay());

		list.add(obj);
		return null;
	}

	public String delete(Integer id) {
		list.removeIf(x -> x.getId() == (id));
		return null;
	}

	public String edit(Person person) {
		int idx = 0;
		int id = 0;
		for (int i = 0; i < list.size(); i++) {
			if (list.get(i).getId() == (person.getId())) {
				id = person.getId();
				idx = i;
				break;
			}
		}

		Person p = new Person();
		p.setId(id);
		p.setName(person.getName());
		p.setJob(person.getJob());
		p.setGender(person.isGender());
		p.setBirthDay(person.getBirthDay());
		list.set(idx, p);

		return null;
	}
}
