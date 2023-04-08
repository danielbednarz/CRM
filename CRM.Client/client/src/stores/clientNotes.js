import { defineStore } from "pinia";
import { api } from "src/boot/axios";

export const useClientNotesStore = defineStore("clientNotes", {
  state: () => ({
    notes: [],
    note: {
      title: "",
      content: "",
      clientId: null,
    },
  }),
  actions: {
    getClientNotes(clientId) {
      api
        .get("/ClientNotes/getClientNotes", { params: { clientId: clientId } })
        .then((response) => {
          this.notes = response.data;
        });
    },
    async addNote() {
      return await api.post("/ClientNotes/add", this.note);
    },
    async deleteNote(noteId) {
      return await api.delete("ClientNotes/delete", {
        params: {
          noteId: noteId,
        },
      });
    },
  },
});
