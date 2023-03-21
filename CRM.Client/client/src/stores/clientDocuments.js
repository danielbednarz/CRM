import { defineStore } from "pinia";
import { api } from "src/boot/axios";

export const useClientDocumentsStore = defineStore("clientDocuments", {
  state: () => ({
    documents: [],
    document: {
      id: null,
      name: '',
      contentType: ''
    },
  }),
  actions: {
    getClientDocuments(clientId) {
      api
        .get("/ClientDocument/getClientDocuments", { params: { clientId: clientId } })
        .then((response) => {
          this.documents = response.data;
        });
    },
    async addDocument() {
      return await api.post("/ClientDocument/add", this.documents);
    },
    async deleteDocument(documentId) {
      return await api.delete("ClientDocument/delete", {
        params: {
          documentId: documentId,
        },
      });
    },
  },
});
